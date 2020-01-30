import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { TodosService } from '../services/todos.service'
import { Todo } from '../models/todo'

@Component({
	selector: 'details-form',
	templateUrl: './details.component.html'
})
export class DetailsComponent implements OnInit {

	private todoForm: FormGroup;

	constructor(private location: Location,
		private service: TodosService,
		private route: ActivatedRoute) {

	}

	get(): void {
		let idString = this.route.snapshot.paramMap.get('id');
		let id = idString != null
			? parseInt(idString)
			: 0;

		this.service.get(id).then(t => {
			this.todoForm.controls.title.setValue(t.title);
			this.todoForm.controls.completed.setValue(t.completed);
		});
	}

	update(todo: Todo): void {
		var idString = this.route.snapshot.paramMap.get('id');
		let id: number = idString != null
			? parseInt(idString)
			: 0;

		this.service.update(id, todo)
			.then(response => this.get());
	}

	ngOnInit(): void {
		this.todoForm = new FormGroup({
			title: new FormControl('title', Validators.required),
			completed: new FormControl('completed', Validators.required)
		});

		this.get();
	}
}