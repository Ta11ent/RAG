import pickle
import uuid
import faiss
from pathlib import Path
from sentence_transformers import SentenceTransformer
import logging
import numpy as np

logging.basicConfig(level=logging.INFO)

class VectorModel:
    def __init__(self, model_name="all-MiniLM-L6-v2"): # dim=384
        self.model = SentenceTransformer(model_name)
        self.dim = self.model.get_sentence_embedding_dimension()
        #self.dim = dim

        base_dir = Path(__file__).resolve().parent
        data_dir = base_dir / "data"
        data_dir.mkdir(parents=True, exist_ok=True)

        self.index_path = data_dir / "index.faiss"
        self.id_map_file = data_dir / "id_map.pkl"
        self.tag_map_file = data_dir / "tag_map.pkl"

        # Инициализация FAISS индекса
        #self.index = faiss.IndexFlatL2(dim)
        self.index = faiss.IndexFlatIP(self.dim)
        self.ids = []  # Список ID
        self.tags = []  # Список тегов

        self._load_index()

    def _load_index(self):
        if self.index_path.exists() and self.id_map_file.exists() and self.tag_map_file.exists():
            self.index = faiss.read_index(str(self.index_path))
            with open(self.id_map_file, "rb") as f:
                self.ids = pickle.load(f)
            with open(self.tag_map_file, "rb") as f:
                self.tags = pickle.load(f)

    def _save_index(self):
        faiss.write_index(self.index, str(self.index_path))
        with open(self.id_map_file, "wb") as f:
            pickle.dump(self.ids, f)
        with open(self.tag_map_file, "wb") as f:
            pickle.dump(self.tags, f)

    def search(self, query: str, top_k: int, tags_filter=None):
        query_vector = self.model.encode([query], convert_to_numpy=True, normalize_embeddings=True)

        if self.index.ntotal == 0:
            logging.warning("FAISS index is empty! No data to search.")
            return []

        distances, indices = self.index.search(query_vector, top_k)

        results = []
        for dist, idx in zip(distances[0], indices[0]):
            if idx == -1 or idx >= len(self.ids):
                continue

            similarity = float(dist)  
            if similarity < 0.4:
                continue

            item_tags = self.tags[idx]
            if tags_filter and not set(tags_filter).issubset(set(item_tags)):
                continue

            results.append({
                "id": self.ids[idx],
                "score": similarity,
                "tags": item_tags
            })

        return results[:top_k]

    def train(self, entries: list[dict]):
        # Генерация уникальных ID для новых записей
        for entry in entries:
            if not entry.get("id"):
                entry["id"] = str(uuid.uuid4())

        # Преобразование текстов в векторы
        texts = [e["text"] for e in entries]
        vectors = self.model.encode(texts, convert_to_numpy=True)

        logging.info(f"Training with {len(entries)} entries")

        # Добавление новых векторов в FAISS
        vectors = vectors / np.linalg.norm(vectors, axis=1, keepdims=True)
        self.index.add(vectors)
        self.ids.extend([entry["id"] for entry in entries])
        self.tags.extend([entry["tags"] for entry in entries])

        # Сохранение индекса
        self._save_index()
        logging.info(f"Trained with {len(entries)} new entries.")

        return [entry["id"] for entry in entries]
