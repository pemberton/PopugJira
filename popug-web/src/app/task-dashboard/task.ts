export interface Task {
    id: number;
    title: string;
    description: string;
    creator: TaskUser;
    assignee: TaskUser;
    created: Date;
    taskState: string;
    closedAt: Date;
    cost: number;
  }

  export interface TaskUser {
    id: number;
    userName: string;
  }