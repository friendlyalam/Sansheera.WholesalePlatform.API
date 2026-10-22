CREATE TABLE IF NOT EXISTS exception_log
(
    id BIGSERIAL PRIMARY KEY,
    timestamp TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    trace_id VARCHAR(100) NULL,
    log_level VARCHAR(50) NOT NULL,
    exception_type VARCHAR(255) NULL,
    message TEXT NOT NULL,
    stack_trace TEXT NULL,
    inner_exception TEXT NULL,
    request_url VARCHAR(500) NULL,
    http_method VARCHAR(10) NULL,
    login_id UUID NULL,
    client_ip VARCHAR(50) NULL,
    user_agent TEXT NULL,
    environment VARCHAR(50) NULL,
    payload JSONB NULL
);

CREATE INDEX IF NOT EXISTS IX_exception_log_timestamp
    ON exception_log (timestamp);

CREATE INDEX IF NOT EXISTS IX_exception_log_trace_id
    ON exception_log (trace_id);

CREATE INDEX IF NOT EXISTS IX_exception_log_exception_type
    ON exception_log (exception_type);

CREATE INDEX IF NOT EXISTS IX_exception_log_login_id
    ON exception_log (login_id);