-- 1. Active Status
INSERT INTO customer_status (id, code, name)
VALUES (1, 'ACTIVE', 'Active')
ON CONFLICT (code) DO NOTHING;

-- 2. Inactive Status
INSERT INTO customer_status (id, code, name)
VALUES (2, 'INACTIVE', 'Inactive')
ON CONFLICT (code) DO NOTHING;

-- 3. Suspended Status
INSERT INTO customer_status (id, code, name)
VALUES (3, 'SUSPENDED', 'Suspended')
ON CONFLICT (code) DO NOTHING;

-- 4. Pending Verification Status
INSERT INTO customer_status (id, code, name)
VALUES (4, 'PENDING_VERIFICATION', 'Pending Verification')
ON CONFLICT (code) DO NOTHING;

-- 5. Blocked Status
INSERT INTO customer_status (id, code, name)
VALUES (5, 'BLOCKED', 'Blocked')
ON CONFLICT (code) DO NOTHING;