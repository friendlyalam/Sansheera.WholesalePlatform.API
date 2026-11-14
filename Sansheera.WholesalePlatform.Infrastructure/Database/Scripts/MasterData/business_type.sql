INSERT INTO business_type (code, name)
VALUES ('HOTEL', 'Hotel')
ON CONFLICT (code) DO NOTHING;

INSERT INTO business_type (code, name)
VALUES ('BAKERY', 'Bakery')
ON CONFLICT (code) DO NOTHING;


INSERT INTO business_type (code, name)
VALUES ('GENERAL_STORE', 'General Store')
ON CONFLICT (code) DO NOTHING;