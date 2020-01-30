import { Component, OnInit } from '@angular/core';
import { TodoService } from './Service/todo.service'
import { Todo } from './Model/todo';

@Component({
	selector: "my-app",
	templateUrl: "/Home/Index"//"./app/app.component.html"
})
export class AppComponent implements OnInit {
	todos: Array<Todo>;
	todoService: TodoService;
	newTodoText = '';

	constructor(todoService: TodoService) {
		this.todoService = todoService;
	}

	ngOnInit(): void {
		this.getTodos();
	}

	getTodos(): void {
		this.todoService
			.getTodos()
			.then(todos => this.todos = todos);
	}

	removeCompleted() {
		this.todoService.removeCompleted();
		this.todos = this.getPending();
	}

	toggleCompletion(todo: Todo) {
		this.todoService.toggleCompletion(todo);
	}

	remove(todo: Todo) {
		this.todoService.remove(todo);
		this.todos.splice(this.todos.indexOf(todo), 1);
	}

	addTodo() {
		if (this.newTodoText.trim().length) {
			this.todoService.add(this.newTodoText).then(res => {
				this.getTodos();
			});
			this.newTodoText = '';
			this.getTodos();
		}
	}

	getPending() {
		return this.todos.filter((todo: Todo) => todo.completed === false);
	}

	getCompleted() {
		return this.todos.filter((todo: Todo) => todo.completed === true);
	}
}