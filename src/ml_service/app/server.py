from concurrent import futures
import grpc
from generated import search_pb2, search_pb2_grpc
from app.model import VectorSearchModel

class VectorSearchService(search_pb2_grpc.VectorSearchServicer):
    def __init__(self):
        self.model = VectorSearchModel()

    def Search(self, request, context):
        result_ids = self.model.search(request.query, request.top_k)
        return search_pb2.SearchResponse(ids=result_ids)

    def Train(self, request, context):
        entries = [{"id": entry.id, "text": entry.text} for entry in request.entries]
        success = self.model.train(entries)
        return search_pb2.TrainResponse(success=success, message="Index updated" if success else "Failed")

def serve():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    search_pb2_grpc.add_VectorSearchServicer_to_server(VectorSearchService(), server)
    server.add_insecure_port('[::]:50051')
    server.start()
    print("gRPC server running on port 50051...")
    server.wait_for_termination()

if __name__ == '__main__':
    serve()
