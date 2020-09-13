import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import { Todo } from '../models/todo'

@Injectable()
export class TodosService {

	private host: string = "http://localhost:62654";

	constructor(private http: Http) { }

	getAll(): Promise<Array<Todo>> {
		return this.http.get(`${this.host}/api/todos`)
			.toPromise()
			.then(response => response.json() as Array<Todo>)
			.catch(this.handleError);
	}

	get(id: number): Promise<Todo> {
		return this.http.get(`${this.host}/api/todos/${id}`)
			.toPromise()
			.then(response => response.json() as Todo)
			.catch(this.handleError);
	}

	add(todo: Todo): Promise<Todo> {

		var headers = new Headers();
		headers.append('Content-Type', 'application/json');

		return this.http.post(`${this.host}/api/todos`, JSON.stringify(todo), { headers: headers })
			.toPromise()
			.then(response => response.json() as Todo)
			.catch(this.handleError);
	}

	update(id: number, todo: Todo): Promise<any> {

		var headers = new Headers();
		headers.append('Content-Type', 'application/json');

		return this.http.put(`${this.host}/api/todos/${id}`, JSON.stringify(todo), { headers: headers })
			.toPromise()
			.catch(this.handleError);
	}

	remove(id: number): Promise<any>  {
		return this.http.delete(`${this.host}/api/todos/${id}`)
			.toPromise()
			.catch(this.handleError);
	}
	
	private handleError(error: any): Promise<any> {
		console.log('An error occurred ' + error);
		return Promise.reject(error.message || error);
	}
}