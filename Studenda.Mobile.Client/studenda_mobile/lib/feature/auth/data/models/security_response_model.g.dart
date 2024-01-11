// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'security_response_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

_$SecurityResponseModelImpl _$$SecurityResponseModelImplFromJson(
        Map<String, dynamic> json) =>
    _$SecurityResponseModelImpl(
      user: UserModel.fromJson(json['User'] as Map<String, dynamic>),
      token: json['Token'] as String,
      refreshToken: json['RefreshToken'] as String,
    );

Map<String, dynamic> _$$SecurityResponseModelImplToJson(
        _$SecurityResponseModelImpl instance) =>
    <String, dynamic>{
      'User': instance.user,
      'Token': instance.token,
      'RefreshToken': instance.refreshToken,
    };
