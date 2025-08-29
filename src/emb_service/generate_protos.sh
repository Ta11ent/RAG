#!/bin/bash
python3 -m grpc_tools.protoc \
  -I./proto \
  --python_out=./app/generated \
  --grpc_python_out=./app/generated \
  ./proto/emb.proto
