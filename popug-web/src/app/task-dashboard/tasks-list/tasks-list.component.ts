import { Component, OnInit } from '@angular/core';
import { Task } from '../task';
import { TaskService } from '../task.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/auth/auth.service';

@Component({
  selector: 'app-tasks-list',
  templateUrl: './tasks-list.component.html',
  styleUrls: ['./tasks-list.component.css']
})
export class TasksListComponent implements OnInit {

  tasks: Task[] = [];
  selectedTask?: Task;
  isManager: boolean = false;

  constructor(private taskService: TaskService,
    private authService: AuthService,
    private router: Router) { }

  ngOnInit() {
    this.getTasks();
    const userRole = this.authService.getUserRole();
    this.isManager = (userRole == 'manager');
  }

  getTasks(): void {
    this.taskService.getTasks()
      .subscribe(tasks => this.tasks = tasks);
  }

  onSelect(task:Task):void {
    this.selectedTask = task;
  }

  onAssignTasks():void {
    this.taskService.assingTasks()
    .toPromise()
    .then(_ =>      
       this.getTasks());
  }
}
