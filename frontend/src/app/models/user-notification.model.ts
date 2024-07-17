export interface UserNotification {
  userId: string;
  message: string;
  isRead: boolean;
}

export interface GoalAchievedNotification {
  userId: string;
  goalName: string;
}

export interface ReportSavedNotification {
  userId: string;
  reportName: string;
}