import 'package:equatable/equatable.dart';

abstract class Failure extends Equatable{
  final String message;
  Failure({required this.message});
  @override
  List<Object?> get props => [];
}

class ServerFailure extends Failure{
  ServerFailure({required super.message});
}

class CacheFailure extends Failure{
  CacheFailure({required super.message});
}

class AuthFailure extends Failure{
  AuthFailure({required super.message});
}
