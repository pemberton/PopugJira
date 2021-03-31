import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task } from './task';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  private taskServiceUrl = 'https://localhost:9002/api/tasks';  // TODO: переделать в бэкэнде

  constructor(
    private http: HttpClient) { }

  getTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(this.taskServiceUrl)
  }

  getTask(id: string): Observable<Task> {
    return this.http.get<Task>(this.taskServiceUrl + '/' + id);
  }

  addTask(task: Task){
    task.assignCost = 0;
    return this.http.put<Task>(this.taskServiceUrl, task);
  }

  editTask(task: Task){
    this.http.post<Task>(this.taskServiceUrl, task);
  }

  assingTasks() {
    return this.http.post(this.taskServiceUrl + "/assign", null);
  }
}
