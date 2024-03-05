export interface AuthRequest
{
  email: string;
  password: string;
}

export interface AuthResponse
{
  id: number;
  role: string;
  username: string;
  email: string;
  phoneNumber: string;
  token: string;
  refreshToken: string;
}