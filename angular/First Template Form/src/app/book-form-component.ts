import { Component } from '@angular/core'; 
import { Book } from './book';

@Component({
    selector: 'book-form',
    templateUrl: './book-form.component.html'
})
export class BookFormComponent { 
    model = new Book(1, 'book name', 'author name', 'publication name is optional');

    onSubmit() {

    }

    newBook() {
        this.model = new Book(0, '', '', '');
    }
}