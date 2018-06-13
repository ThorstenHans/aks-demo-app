const colors = require('colors');

class LogService {
    info(message) {
        console.info(`${colors.green('INFO:')} ${colors.white(message)}`);
    }

    warn(message) {
        console.info(`${colors.yellow('WARN:')} ${colors.white(message)}`);
    }

    error(message) {
        console.info(`${colors.red('ERROR:')} ${colors.white(message)}`);
    }

    dir(something) {
        console.dir(something);
    }
}

module.exports = new LogService();
