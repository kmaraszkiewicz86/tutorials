export class Todo {
	id: number;
	title: string;
	private _completed: boolean;

	constructor(id: number, title: string, completed: boolean) {
		this.id = id;
		this.title = title;
		this.completed = false;
	}

	set completed(value: boolean) {
		this._completed = value;
	}

	get completed(): boolean {
		return this._completed;
	}
}