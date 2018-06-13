const PDFDocument = require('pdfkit');
const logService = require('./log.service');
class PdfService {
    constructor(config) {
        this.config = config;
    }

    generatePdf(session) {
        return new Promise((resolve, reject) => {
            logService.info(`Generating new PDF document for session '${session.title}'`);
            const doc = new PDFDocument();

            doc.fontSize(20);
            doc.text(session.title);
            doc.moveDown();
            doc.fontSize(10);
            doc.fillColor('grey');
            doc.text(session.speaker);
            doc.moveDown();
            doc.moveDown();

            doc.fillColor('black');
            doc.fontSize(14);
            doc.text(session.abstract, {
                lineGap: 6,
            });
            doc.end();
            logService.info(`PDF document generated successfully`);
            resolve(doc);
        });
    }
}

module.exports = PdfService;
