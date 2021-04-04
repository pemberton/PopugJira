import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Task } from '../task';
import { TaskService } from '../task.service';

@Component({
  selector: 'app-task-add',
  templateUrl: './task-add.component.html',
  styleUrls: ['./task-add.component.css']
})
export class TaskAddComponent implements OnInit {

  @Input() task?: Task;

  constructor(
    private route: ActivatedRoute,
    private taskService: TaskService,
    private router: Router) {
      this.task = {} as Task;
     }

  ngOnInit(): void {
  }

  add(task: Task): void {          
        this.taskService.addTask(task)
        .toPromise()
        .then(_ => this.router.navigate(['tasks']));
  }
}
