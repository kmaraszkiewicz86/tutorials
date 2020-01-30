import { TodoService } from './todo.service';

export class MockTodoService extends TodoService {
    getPending () {
        return [];
    }
}