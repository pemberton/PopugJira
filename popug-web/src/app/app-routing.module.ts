import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { TaskAddComponent } from './task-dashboard/task-add/task-add.component';
import { TasksListComponent } from './task-dashboard/tasks-list/tasks-list.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'tasks', component: TasksListComponent },
  { path: 'detail', component: TaskAddComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
