import { Component, OnInit } from '@angular/core';
import { TodosService } from '../services/todos.service'
import { Todo } from '../models/todo'

@Component({
	selector: 'list',
	templateUrl: './list.component.html'
})
export class ListComponent implements OnInit {

	todos: Array<Todo>;
	text: string = "";

	constructor(private todoService: TodosService) {
		this.todos = new Array();
	}

	getAll() : void  {
		this.todoService.getAll()
			.then(todos => {
				this.todos = todos;
				console.log(this.todos);
			});
	}

	add(text: string): void {
		if (this.text == undefined || this.text.trim() == "") {
			console.log("undefined");
			return;
		}
			
		this.todoService.add(new Todo(this.text, false, new Date()))
			.then(res => this.getAll());
	}

	remove(todo: Todo): void {
		console.log(todo);

		this.todoService.remove(todo.id)
			.then(() => this.getAll());
	}

	ngOnInit(): void {
		this.getAll();
	}
}