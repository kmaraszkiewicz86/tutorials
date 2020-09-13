import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/of';

import { Todo } from '../Model/todo'

@Injectable()
export class TodoService {
	todos: Array<Todo>;

	constructor(
		private http: Http) {

	}

	private handleError(error: any): Promise<any> {
		console.error('An error occurred', error); // for demo purposes only
		return Promise.reject(error.message || error);
	}

	getTodos(): Promise<Array<Todo>> {
		return this.http.get('/api/todos')
			.toPromise()
			.then(response => response.json() as Array<Todo>)
			.catch(this.handleError);
	}

	getPendingTodos() {
		this.http.get('/api/todos/pending-only')
			.subscribe(err => console.log(err),
			() => console.log('getTodos completed'));
	}

	removeCompleted() {
		this.getPendingTodos();
	}

	postTodo(todo: Todo): Promise<Todo> {
		var headers = new Headers();
		headers.append('Content-Type', 'application/json');

		return this.http.post('api/todos', JSON.stringify(todo), { headers: headers })
			.toPromise()
			.then(response => response.json() as Todo)
			.catch(this.handleError);
	}

	putTodo(todo: Todo) {
		var headers = new Headers();
		headers.append("Content-Type", "application/json");

		this.http.put('/api/todos/' + todo.id, JSON.stringify(todo), { headers: headers })
			.toPromise()
			.then(() => todo)
			.catch(this.handleError);
	}

	deleteTodo(todo: Todo) {
		this.http.delete('/api/todos/' + todo.id)
			.subscribe(err => console.log(err),
			() => console.log('delete completed'));
	}

	remove(todo: Todo) {
		this.deleteTodo(todo);
	}

	add(title: string): Promise<Todo> {
		var todo = new Todo(0, title, false);
		return this.postTodo(todo);
	}

	toggleCompletion(todo: Todo) {
		todo.completed = !todo.completed;
		this.putTodo(todo);
	}
}