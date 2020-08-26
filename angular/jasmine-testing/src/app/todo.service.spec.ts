import { TestBed, ComponentFixture } from '@angular/core/testing'

import { TodoService } from './todo.service';
import { MockTodoService } from './mocktodo.service';

describe("Todo service testing", () => {
    let service: TodoService;
    let mockService: MockTodoService;

    beforeEach(() => 
    { 
        service = new TodoService();
        mockService = new MockTodoService();
    });

    it('getPending length cossss', () => {
        expect(service.getPending().length).toBe(3);
    })

    it ('asasdasd', () => {
        expect(mockService.getPending().length).toBe(0);
    });

    it('getCompleted length should return 3', () => {
        expect(mockService.getCompleted().length).toBe(0);
    });
});