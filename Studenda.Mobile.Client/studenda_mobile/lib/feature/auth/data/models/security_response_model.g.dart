// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'security_response_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

_$SecurityResponseModelImpl _$$SecurityResponseModelImplFromJson(
        Map<String, dynamic> json) =>
    _$SecurityResponseModelImpl(
      user: UserModel.fromJson(json['user'] as Map<String, dynamic>),
      token: json['token'] as String,
      refreshToken: json['refreshToken'] as String,
    );

Map<String, dynamic> _$$SecurityResponseModelImplToJson(
        _$SecurityResponseModelImpl instance) =>
    <String, dynamic>{
      'user': instance.user,
      'token': instance.token,
      'refreshToken': instance.refreshToken,
    };
