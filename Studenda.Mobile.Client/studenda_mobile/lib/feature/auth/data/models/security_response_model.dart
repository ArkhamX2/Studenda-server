import 'package:freezed_annotation/freezed_annotation.dart';
import 'package:studenda_mobile/feature/auth/data/models/user_model.dart';

part 'security_response_model.freezed.dart';
part 'security_response_model.g.dart';

@freezed
class SecurityResponseModel with _$SecurityResponseModel{
  const factory SecurityResponseModel({

    @JsonKey(name: 'User') required UserModel user,
    @JsonKey(name: 'Token') required String token,
    @JsonKey(name: 'RefreshToken') required String refreshToken,
  }) = _SecurityResponseModel;
  
  factory SecurityResponseModel.fromJson(Map<String,dynamic> json) => _$SecurityResponseModelFromJson(json);
}
