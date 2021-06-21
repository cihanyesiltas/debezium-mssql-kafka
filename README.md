# debezium-mssql-kafka

### 1. Use below commands to start containers

```console
export DEBEZIUM_VERSION=1.6
docker-compose build
docker-compose up
```

### 2. Use jobs.sql file to create database and insert sample data

### 3. Create kafka connector for jobs table

```bash
curl --location --request POST 'http://localhost:8083/connectors' \
--header 'Content-Type: application/json' \
--data-raw '{
    "name": "debezium-sqlserver-connector",
    "config": {
        "connector.class" : "io.debezium.connector.sqlserver.SqlServerConnector",
        "tasks.max" : "1",
        "database.server.name" : "debezium",
        "database.hostname" : "sqlserver",
        "database.port" : "1433",
        "database.user" : "sa",
        "database.password" : "Password!",
        "database.dbname" : "testDB",
        "database.history.kafka.bootstrap.servers" : "kafka:29092",
        "database.history.kafka.topic": "testDB.changes",
        "heartbeat.interval.ms": "5000",
        "key.converter": "org.apache.kafka.connect.json.JsonConverter",
        "key.converter.schemas.enable": "false",
        "value.converter": "org.apache.kafka.connect.json.JsonConverter",
        "value.converter.schemas.enable": "false",
        "table.include.list":"dbo.jobs",
        "column.blacklist": "dbo.jobs.taxNumber",
        "snapshot.select.statement.overrides":"dbo.jobs",
        "snapshot.select.statement.overrides.dbo.jobs":"SELECT * FROM dbo.jobs WHERE id>103",
        "tombstones.on.delete": false
    }
}'
```

### 4. Consume messages from specified topic

```console
docker-compose -f docker-compose.yaml exec kafka /kafka/bin/kafka-console-consumer.sh \
    --bootstrap-server kafka:29092 \
    --from-beginning \
    --property print.key=true \
    --topic debezium.dbo.jobs
```

### 5. List all topics

```console
docker-compose -f docker-compose.yaml exec kafka /kafka/bin/kafka-topics.sh --list --zookeeper  zookeeper:2181    
```

### 6. Remove kafka topic

```console
docker-compose -f docker-compose.yaml exec kafka /kafka/bin/kafka-topics.sh --zookeeper zookeeper:2181 \
                --topic debezium.dbo.jobs \
                --delete
```
