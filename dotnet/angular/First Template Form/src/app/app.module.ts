import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './AppComponent';
import { BookFormComponent } from './book-form-component';

@NgModule({
    imports: [ BrowserModule, FormsModule ],
    declarations: [ AppComponent, BookFormComponent ],
    bootstrap: [ AppComponent ]
})
export class AppModule { }