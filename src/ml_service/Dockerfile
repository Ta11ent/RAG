FROM python:3.13-slim as builder

WORKDIR /app

RUN apt-get update && \
    apt-get install -y --no-install-recommends build-essential && \
    rm -rf /var/lib/apt/lists/*  # Очистить кэш apt

COPY requirements.txt . 
RUN pip install --no-cache-dir -r requirements.txt  # Устанавливаем зависимости

FROM python:3.13-slim

WORKDIR /app

COPY --from=builder /app /app

COPY . . 

ARG PORT=50051
ENV PORT=${PORT}

EXPOSE $PORT

CMD ["python", "-u", "server.py"]
