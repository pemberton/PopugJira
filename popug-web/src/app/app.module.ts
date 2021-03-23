import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TasksListComponent } from './task-dashboard/tasks-list/tasks-list.component';
import { HttpClientModule } from '@angular/common/http';
import { TaskDetailComponent } from './task-dashboard/task-detail/task-detail.component';
import { FormsModule } from '@angular/forms';
import { TaskAddComponent } from './task-dashboard/task-add/task-add.component';

@NgModule({
  declarations: [
    AppComponent,
    TasksListComponent,
    TaskDetailComponent,
    TaskAddComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
