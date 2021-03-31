import { Component, OnInit } from '@angular/core';
import { Task } from '../task';
import { TaskService } from '../task.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-tasks-list',
  templateUrl: './tasks-list.component.html',
  styleUrls: ['./tasks-list.component.css']
})
export class TasksListComponent implements OnInit {

  tasks: Task[] = [];
  selectedTask?: Task;

  constructor(private taskService: TaskService,
    private router: Router) { }

  ngOnInit() {
    this.getTasks();
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
    .then(_ => this.router.navigate(['tasks']));;
  }
}
