CREATE TABLE IF NOT EXISTS user_credential
(
    id UUID NOT NULL DEFAULT gen_random_uuid(),

    user_account_id UUID NOT NULL,

    -- MPIN Authentication
    is_mpin_enabled BOOLEAN NOT NULL DEFAULT FALSE,
    mpin_hash VARCHAR(255),
    last_mpin_changed_at TIMESTAMPTZ,
    failed_mpin_attempts SMALLINT NOT NULL DEFAULT 0,
    mpin_lockout_until TIMESTAMPTZ,

    -- OTP Authentication
    otp_hash VARCHAR(255),
    otp_expires_at TIMESTAMPTZ,
    otp_failed_attempts SMALLINT NOT NULL DEFAULT 0,
    otp_lockout_until TIMESTAMPTZ,
    otp_verified_at TIMESTAMPTZ,

    -- Audit
    created_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    created_by UUID,
    updated_at TIMESTAMPTZ,
    updated_by UUID,

    CONSTRAINT pk_user_credential
        PRIMARY KEY (id),

    CONSTRAINT fk_user_credential_user_account
        FOREIGN KEY (user_account_id)
        REFERENCES user_account(id)
        ON DELETE CASCADE,

    CONSTRAINT uq_user_credential_user_account
        UNIQUE (user_account_id)
);