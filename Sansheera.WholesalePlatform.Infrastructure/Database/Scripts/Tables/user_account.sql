CREATE TABLE IF NOT EXISTS user_account
(
    id UUID NOT NULL DEFAULT gen_random_uuid(),

    -- User Profile Mapping
    user_type VARCHAR(30) NOT NULL,--employee,customer,admin
    customer_id UUID,
    employee_id UUID,

    -- Login Identity
    phone_number VARCHAR(15) NOT NULL,
    is_phone_verified BOOLEAN NOT NULL DEFAULT FALSE,
    phone_verified_at TIMESTAMPTZ,

    -- Account Status
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    last_login_at TIMESTAMPTZ,

    -- Audit
    created_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    created_by UUID,
    updated_at TIMESTAMPTZ,
    updated_by UUID,

    CONSTRAINT pk_user_account
        PRIMARY KEY (id)
);