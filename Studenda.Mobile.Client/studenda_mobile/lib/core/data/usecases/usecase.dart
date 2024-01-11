import 'package:dartz/dartz.dart';
import 'package:studenda_mobile/core/data/error/failure.dart';

abstract class Usecase<Type,Params>{
  Future<Either<Failure,Type>> call(Params params);
}
