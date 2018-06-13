const logService = require('./log.service');
const MailGun = require('mailgun-js');
class NotificationService {
    constructor(config) {
        this.config = config;
        this.mailGunService = MailGun({
            apiKey: this.config.mailGunApiKey,
            domain: this.config.mailGunAccount,
        });
    }

    notifyUser(blobUrl) {
        return new Promise((resolve, reject) => {
            const data = {
                from: 'sessions@sandbox15234.mailgun.org',
                to: this.config.recipient,
                subject: 'Sessions Demo: The session has been exported',
                html: `Hey, 
                    you can download the session from <a href="${blobUrl}">here</a>.
                    
                    Thank you!`,
            };
            this.mailGunService.messages().send(data, (err, body) => {
                if (err) {
                    logService.warn('There was a problem while notifying the user.');
                    reject(err);
                } else {
                    logService.info(`Submitted mail with identifier '${body ? body.id : 'null'}'`);
                    logService.info(blobUrl);
                    resolve();
                }
            });
        });
    }
}

module.exports = NotificationService;
