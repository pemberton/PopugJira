export interface Task {
    id: number;
    title: string;
    description: string;
    creator: User;
  }

  export interface User {
    id: number;
    name: string;
  }