const azure = require('azure-storage');
const newGuid = require('uuid/v4');
const logService = require('./log.service');

class BlobService {
    constructor(config) {
        this.config = config;
        this.containerName = 'sessions-as-pdf';
        this.blobName = `${newGuid()}.pdf`;
        this.azureBlobService = azure.createBlobService(config.azureStorageAccount, config.azureStorageAccountKey);
    }

    upladToAzureBlobStorage(pdfStream) {
        return this._createContainer().then(() => {
            return this._uploadBlobToContainer(pdfStream);
        });
    }
    _uploadBlobToContainer(pdfStream) {
        return new Promise((resolve, reject) => {
            this.azureBlobService.createBlockBlobFromStream(this.containerName, this.blobName, pdfStream, pdfStream.readableLength, err => {
                if (err) {
                    logService.warn(`Error while uploading Blob ${err}`);
                    reject(err);
                } else {
                    logService.info(`Session stored on Azure Storage as Blob with name ${this.blobName}`);
                    resolve(this.azureBlobService.getUrl(this.containerName, this.blobName));
                }
            });
        });
    }
    _createContainer() {
        return new Promise((resolve, reject) => {
            this.azureBlobService.createContainerIfNotExists(this.containerName, { publicAccessLevel: 'blob' }, err => {
                if (err) {
                    logService.warn(`Error while creating Blob Container: ${err}`);
                    reject(err);
                } else {
                    logService.info(`BlobContainer ${this.containerName} is available`);
                    resolve();
                }
            });
        });
    }
}

module.exports = BlobService;
