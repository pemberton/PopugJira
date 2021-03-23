import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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
    private taskService: TaskService) {
      this.task = {} as Task;
     }

  ngOnInit(): void {
    this.getTask();
  }

  getTask(): void {
    const id = this.route.snapshot.paramMap.get('id');

    if (id != null)
        this.taskService.getTask(id)
              .subscribe(task => this.task = task);      
  }

}