INSERT INTO customer_status (code, name)
VALUES ('PENDING', 'Pending')
ON CONFLICT (code) DO NOTHING;


INSERT INTO customer_status (code, name)
VALUES ('ACTIVE', 'Active')
ON CONFLICT (code) DO NOTHING;


INSERT INTO customer_status (code, name)
VALUES ('INACTIVE', 'Inactive')
ON CONFLICT (code) DO NOTHING;

INSERT INTO customer_status (code, name)
VALUES ('BLOCKED', 'Blocked')
ON CONFLICT (code) DO NOTHING;