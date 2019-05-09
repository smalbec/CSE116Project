const sid = require('shortid');

module.exports = class Player{
    constructor() {
        this.id = sid.generate();
    }
};