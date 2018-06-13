const sql = require('mssql');
const Session = require('./../models/session');
const logService = require('./log.service');

class DatabaseService {
    constructor(config) {
        this.config = config;
    }
    getSession() {
        const _config = {
            user: this.config.sqlUser,
            password: this.config.sqlPwd,
            server: this.config.sqlHost,
            database: 'sessions',
            options: {
                encrypt: true,
            },
        };
        return sql
            .connect(_config)
            .then(pool => {
                logService.info('Established connection to SQL Azure.');
                return pool
                    .request()
                    .input('id', sql.UniqueIdentifier, this.config.sessionId)
                    .query('SELECT TITLE, ABSTRACT, SPEAKER FROM SESSIONS WHERE ID =@id')
                    .then(results => {
                        logService.info(`Found ${results.recordset.length} session(s) in database matching id '${this.config.sessionId}'`);
                        return new Session(results.recordset[0].TITLE, results.recordset[0].ABSTRACT, results.recordset[0].SPEAKER);
                    })
                    .catch(error => {
                        logService.error(`There was a problem when executing TSQL on SQL Azure instance ${error}`);
                    });
            })
            .catch(err => {
                logService.error(err);
            });
    }
}
module.exports = DatabaseService;
