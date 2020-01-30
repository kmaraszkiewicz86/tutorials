import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { ListComponent } from './components/list/list.component';
import { DetailsComponent } from './components/details/details.component';
import { TodosService } from './components/services/todos.service'

@NgModule({
    declarations: [
		AppComponent,
		ListComponent,
		DetailsComponent
	],
	providers: [TodosService],
    imports: [
        CommonModule,
        HttpModule, 
		FormsModule,
		ReactiveFormsModule,
        RouterModule.forRoot([
			{ path: '', redirectTo: 'home', pathMatch: 'full' },
			{ path: 'home', component: ListComponent },
			{ path: 'details/:id', component: DetailsComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
