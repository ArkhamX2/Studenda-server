import 'package:freezed_annotation/freezed_annotation.dart';

part 'security_request_model.freezed.dart';
part 'security_request_model.g.dart';

@freezed
class SecurityRequestModel with _$SecurityRequestModel{
  const factory SecurityRequestModel({
    required String email,
    required String password,
    required String roleName,
  }) = _SecurityRequestModel;

  factory SecurityRequestModel.fromJson(Map<String,dynamic> json) => _$SecurityRequestModelFromJson(json);
}
