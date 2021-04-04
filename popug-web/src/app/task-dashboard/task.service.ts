import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task } from './task';
import { AuthService } from '../auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  private taskServiceUrl = 'https://localhost:9002/api/tasks';

  constructor(
    private authService: AuthService,
    private http: HttpClient) { }

  getTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(this.taskServiceUrl+ '/' + this.authService.getUserId())
  }

  addTask(task: Task){
    task.cost = 0;
    return this.http.put<Task>(this.taskServiceUrl, task);
  }

  editTask(task: Task){
    this.http.post<Task>(this.taskServiceUrl, task);
  }

  assingTasks() {
    return this.http.post(this.taskServiceUrl + "/assign", null)
  }
}
