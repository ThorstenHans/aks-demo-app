const config = require('./config/config')();
const DatabaseService = require('./services/database.service');
const PdfService = require('./services/pdf.service');
const BlobService = require('./services/blob.service');
const NotificationService = require('./services/notification.service');
const logService = require('./services/log.service');

if (!config.isValid()) {
    logService.error('Got invalid configuration, job will be terminated');
}
const databaseService = new DatabaseService(config);
const pdfService = new PdfService(config);
const blobService = new BlobService(config);
const notificationService = new NotificationService(config);
const logServiuce = require('./services/log.service');

databaseService
    .getSession()
    .then(session => {
        return pdfService.generatePdf(session);
    })
    .then(pdfStream => {
        return blobService.upladToAzureBlobStorage(pdfStream);
    })
    .then(blobUrl => {
        return notificationService.notifyUser(blobUrl);
    })
    .then(() => {
        process.exit(0);
    })
    .catch(err => {
        logService.error('Error raised');
        logService.dir(err);
        process.exit(1);
    });
