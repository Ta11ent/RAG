import pickle
import uuid
import faiss
from pathlib import Path
from sentence_transformers import SentenceTransformer


class VectorModel:
    def __init__(self, model_name="all-MiniLM-L6-v2", dim=384):
        self.model = SentenceTransformer(model_name)
        self.dim = dim

        base_dir = Path(__file__).resolve().parent  # app/
        data_dir = base_dir / "data"
        data_dir.mkdir(parents=True, exist_ok=True)

        self.index_path = data_dir / "index.faiss"
        self.data_path = data_dir / "data.pkl"

        self.index = faiss.IndexFlatL2(dim)
        self.data = []

        self._load_index()

    def _load_index(self):
        if self.index_path.exists() and self.data_path.exists():
            self.index = faiss.read_index(str(self.index_path))
            with open(self.data_path, "rb") as f:
                self.data = pickle.load(f)

    def _save_index(self):
        faiss.write_index(self.index, str(self.index_path))
        with open(self.data_path, "wb") as f:
            pickle.dump(self.data, f)

    def search(self, query: str, top_k: int, tags_filter=None):
        vector = self.model.encode([query], convert_to_numpy=True)
        distances, indices = self.index.search(vector, top_k)

        results = []
        for dist, idx in zip(distances[0], indices[0]):
            if idx == -1 or idx >= len(self.data):
                continue
            item = self.data[idx]

            if tags_filter and not set(tags_filter).issubset(set(item.get("tags", []))):
                continue

            results.append({
                "id": item["id"],
                "text": item["text"],
                "tags": item.get("tags", []),
                "score": float(dist)
            })
        return results

    def train(self, entries: list[dict]):
        for entry in entries:
            if not entry.get("id"):
                entry["id"] = str(uuid.uuid4())

        texts = [e["text"] for e in entries]
        vectors = self.model.encode(texts, convert_to_numpy=True)

        self.index.add(vectors)
        self.data.extend(entries)
        self._save_index()
        return [e["id"] for e in entries]