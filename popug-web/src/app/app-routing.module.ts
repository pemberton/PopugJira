import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TaskAddComponent } from './task-dashboard/task-add/task-add.component';
import { TaskDetailComponent } from './task-dashboard/task-detail/task-detail.component';
import { TasksListComponent } from './task-dashboard/tasks-list/tasks-list.component';

const routes: Routes = [
  { path: 'tasks', component: TasksListComponent },
  { path: 'detail', component: TaskAddComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
