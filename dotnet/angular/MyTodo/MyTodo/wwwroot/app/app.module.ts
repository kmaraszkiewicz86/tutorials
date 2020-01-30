import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { TodoService } from './Service/todo.service';
import { HttpModule } from '@angular/http';

@NgModule({
	imports: [BrowserModule, FormsModule, HttpModule],
	providers: [TodoService],
	declarations: [AppComponent],
	bootstrap: [AppComponent]
})
export class AppModule { }