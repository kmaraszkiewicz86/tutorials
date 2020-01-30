"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Todo = /** @class */ (function () {
    function Todo(id, title, completed) {
        this.id = id;
        this.title = title;
        this.completed = false;
    }
    Object.defineProperty(Todo.prototype, "completed", {
        get: function () {
            return this._completed;
        },
        set: function (value) {
            this._completed = value;
        },
        enumerable: true,
        configurable: true
    });
    return Todo;
}());
exports.Todo = Todo;
//# sourceMappingURL=todo.js.map