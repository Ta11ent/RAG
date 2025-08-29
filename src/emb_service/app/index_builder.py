import json
import pickle
import faiss
from sentence_transformers import SentenceTransformer
from pathlib import Path
import numpy as np

model = SentenceTransformer("all-mpnet-base-v2")
#all-MiniLM-L6-v2

base_dir = Path(__file__).resolve().parent
data_dir = base_dir / "data"
data_dir.mkdir(parents=True, exist_ok=True)

data_file = data_dir / "sample_data.json"
with open(data_file, encoding="utf-8") as f:
    data = json.load(f)

texts = [item["text"] for item in data]  
ids = [item["id"] for item in data]
tags = [item["tags"] for item in data] 

#embeddings = model.encode(texts, convert_to_numpy=True)
#index = faiss.IndexFlatL2(embeddings.shape[1])
embeddings = model.encode(texts, convert_to_numpy=True, normalize_embeddings=True)
index = faiss.IndexFlatIP(embeddings.shape[1]) 
index.add(embeddings)

index_file = data_dir / "index.faiss"
faiss.write_index(index, str(index_file))

id_map_file = data_dir / "id_map.pkl"
with open(id_map_file, "wb") as f:
    pickle.dump(ids, f)

tag_map_file = data_dir / "tag_map.pkl"
with open(tag_map_file, "wb") as f:
    pickle.dump(tags, f)