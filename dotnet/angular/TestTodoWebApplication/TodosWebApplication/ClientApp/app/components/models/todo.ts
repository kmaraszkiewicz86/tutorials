
export class Todo {

	public id: number;

	public title: string;

	public completed: boolean;

	public createTime: Date;

	constructor(title: string, completed: boolean, createTime: Date) {
		this.title = title;
		this.completed = completed;
		this.createTime = createTime;
	}
}