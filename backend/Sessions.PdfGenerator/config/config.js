class Config {
    constructor(azAccount, azKey, sqlHost, sqlUserName, sqlPwd, mailGunAccount, mailGunApiKey, sessionId, recipient) {
        this.azureStorageAccount = azAccount;
        this.azureStorageAccountKey = azKey;
        this.sqlHost = sqlHost;
        this.sqlUser = sqlUserName;
        this.sqlPwd = sqlPwd;
        this.mailGunAccount = mailGunAccount;
        this.mailGunApiKey = mailGunApiKey;
        this.sessionId = sessionId;
        this.recipient = recipient;
    }

    isValid() {
        return (
            !!this.azureStorageAccount &&
            !!this.azureStorageAccountKey &&
            !!this.sqlHost &&
            !!this.sqlUser &&
            !!this.sqlPwd &&
            !!this.sessionId
        );
    }
}

const readConfig = () => {
    return new Config(
        process.env['AZ_SESSION_STORAGE']
        process.env['AZ_SESSION_STORAGE_KEY']
        process.env['SQL_AZ_HOST'],
        process.env['SQL_AZ_USER'],
        process.env['AZ_SQL_PWD']
        process.env['MAILGUN_DOMAIN'],
        process.env['MAILGUN_API_KEY'],
        process.env['SESSION_ID'],
        process.env['RECIPIENT_MAIL_ADDR']
        'thorsten.hans@thinktecture.com'
    );
};

module.exports = readConfig;
