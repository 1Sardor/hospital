from rest_framework import serializers
from main.models import *


class DoctorSerializer(serializers.ModelSerializer):
    class Meta:
        depth = 1
        model = Doctor
        fields = '__all__'
        extra_kwargs = {'password': {'write_only': True}}


class DirectionSerializer(serializers.ModelSerializer):
    class Meta:
        model = Direction
        fields = '__all__'


class ProvinceSerializer(serializers.ModelSerializer):
    class Meta:
        model = Province
        fields = '__all__'


class RegionSerializer(serializers.ModelSerializer):
    class Meta:
        model = Region
        fields = '__all__'


class HospitalSerializer(serializers.ModelSerializer):
    class Meta:
        depth = 1
        model = Hospital
        fields = '__all__'


class PatientSerializer(serializers.ModelSerializer):
    class Meta:
        model = Patient
        fields = '__all__'


class RetseptsSerializer(serializers.ModelSerializer):
    class Meta:
        depth = 2
        model = Retsepts
        fields = '__all__'


class CommentsSerializer(serializers.ModelSerializer):
    class Meta:
        model = Comments
        fields = '__all__'


class DrugsSerializer(serializers.ModelSerializer):
    class Meta:
        depth = 2
        model = Drugs
        fields = '__all__'


class PaymentSerializer(serializers.ModelSerializer):
    class Meta:
        model = Drugs
        fields = '__all__'


class PaymentTypesSerializer(serializers.ModelSerializer):
    class Meta:
        depth = 1
        model = PaymentTypes
        fields = '__all__'


class QueueSerializer(serializers.ModelSerializer):
    class Meta:
        depth = 1
        model = Queue
        fields = '__all__'


class RedirectionSerializer(serializers.ModelSerializer):
    class Meta:
        depth = 1
        model = Redirection
        fields = '__all__'


class SalarySerializer(serializers.ModelSerializer):
    class Meta:
        depth = 1
        model = Salary
        fields = '__all__'


class BonusSerializer(serializers.ModelSerializer):
    class Meta:
        depth = 1
        model = Bonus
        fields = '__all__'
