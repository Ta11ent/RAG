import json, pickle
import faiss
from model import VectorSearchModel
from sentence_transformers import SentenceTransformer
from pathlib import Path

model = SentenceTransformer("all-MiniLM-L6-v2")

base_dir = Path(__file__).resolve().parent  
data_file = base_dir / "app" / "data" / "sample_data.json"

with open(data_file, encoding="utf-8") as f:
    data = json.load(f)

texts = [item["text"] for item in data]
ids = [item["id"] for item in data]

embeddings = model.encode(texts, convert_to_numpy=True)
index = faiss.IndexFlatL2(embeddings.shape[1])
index.add(embeddings)

index_file = base_dir / "app" / "data" / "index.faiss"
faiss.write_index(index, str(index_file))

id_map_file = base_dir / "app" / "data" / "id_map.pkl"
with open(id_map_file, "wb") as f:
    pickle.dump(ids, f)
