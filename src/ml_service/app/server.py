import os
from concurrent import futures
import grpc
from generated import ml_pb2, ml_pb2_grpc
from model import VectorModel


class VectorService(ml_pb2_grpc.VectorServicer):
    def __init__(self):
        self.model = VectorModel()

    def Search(self, request, context):
        tags = list(request.tags)
        results = self.model.search(request.query, request.top_k, tags_filter=tags)

        search_results = []
        for r in results:
            search_results.append(ml_pb2.SearchResult(
                id=r["id"],
                #text=r["text"],
                #score=r["score"],
                #tags=r["tags"]
            ))

        return ml_pb2.SearchResponse(results=search_results)

    def Train(self, request, context):
        entries = []
        for entry in request.entries:
            entries.append({
                "text": entry.text,
                "tags": list(entry.tags)
            })

        ids = self.model.train(entries)
        return ml_pb2.TrainResponse(success=True, ids=ids)


def serve():
    port = os.getenv("PORT", 50051)
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    ml_pb2_grpc.add_VectorServicer_to_server(VectorService(), server)
    server.add_insecure_port(f'[::]:{port}')
    server.start()
    print(f"gRPC server running on port {port}...")
    server.wait_for_termination()


if __name__ == '__main__':
    serve()