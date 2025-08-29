FROM python:3.13-slim AS builder

WORKDIR /app

RUN apt-get update && \
    apt-get install -y --no-install-recommends \
        build-essential \
        git \
    && rm -rf /var/lib/apt/lists/*

COPY requirements.txt .
RUN pip install --no-cache-dir --upgrade pip && \
    pip install --no-cache-dir --prefix=/install -r requirements.txt

# Финальный слой
FROM python:3.13-slim

WORKDIR /app

COPY --from=builder /install /usr/local
COPY . .

ENV PYTHONPATH=/app

ARG PORT=50051
ENV PORT=${PORT}

EXPOSE $PORT

CMD ["python", "-u", "app/server.py"]