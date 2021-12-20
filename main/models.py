from django.db import models
from django.contrib.auth.models import AbstractUser


class Direction(models.Model):
    name = models.CharField(max_length=255)

    def __str__(self):
        return self.name


class Province(models.Model):
    name = models.CharField(max_length=255)

    def __str__(self):
        return self.name


class Region(models.Model):
    province = models.ForeignKey(Province, on_delete=models.CASCADE)
    name = models.CharField(max_length=255)

    def __str__(self):
        return self.province.name + ' + ' + self.name


class Hospital(models.Model):
    type = models.IntegerField(choices=(
        (1, 'state'),
        (2, 'private'),
    ))
    name = models.CharField(max_length=255, unique=True)
    phone = models.CharField(max_length=255)
    region = models.ForeignKey(Region, on_delete=models.CASCADE)
    inn = models.CharField(max_length=255)

    def __str__(self):
        return self.name


class Doctor(AbstractUser):
    type = models.IntegerField(choices=(
        (1, 'Accountant'),
        (2, 'Doctor'),
    ))
    phone = models.CharField(max_length=25, null=True, blank=True)
    direction = models.ForeignKey(Direction, on_delete=models.CASCADE, null=True, blank=True)
    hospital = models.ForeignKey(Hospital, on_delete=models.CASCADE, null=True, blank=True)
    room = models.CharField(max_length=255)

    def __str__(self):
        return self.username

    class Meta(AbstractUser.Meta):
        swappable = 'AUTH_USER_MODEL'
        verbose_name = 'Doctors'
        verbose_name_plural = 'Doctors'


class Patient(models.Model):
    name = models.CharField(max_length=255)
    hospital = models.ForeignKey(Hospital, on_delete=models.CASCADE, null=True, blank=True)
    phone = models.IntegerField()
    birthday = models.DateField()

    def __str__(self):
        return self.name


class Retsepts(models.Model):
    doctor = models.ForeignKey(Doctor, on_delete=models.CASCADE)
    patient = models.ForeignKey(Patient, on_delete=models.CASCADE)
    date = models.DateTimeField(auto_now_add=True)

    def __str__(self):
        return self.patient.name


class Drugs(models.Model):
    retsept = models.ForeignKey(Retsepts, on_delete=models.CASCADE)
    name = models.CharField(max_length=255)
    duration = models.CharField(max_length=255)


class Comments(models.Model):
    patient = models.ForeignKey(Patient, on_delete=models.CASCADE)
    doctor = models.ForeignKey(Doctor, on_delete=models.CASCADE)
    ill = models.CharField(max_length=255)
    date = models.DateTimeField(auto_now_add=True)

    def __str__(self):
        return self.patient.name


class Payment(models.Model):
    patient = models.ForeignKey(Patient, on_delete=models.CASCADE)
    summa = models.IntegerField()
    date = models.DateTimeField(auto_now_add=True)


class PaymentTypes(models.Model):
    payment = models.ForeignKey(Payment, on_delete=models.CASCADE)
    name = models.CharField(max_length=255)
    summa = models.IntegerField()


class Queue(models.Model):
    hospital = models.ForeignKey(Hospital, on_delete=models.CASCADE)
    number = models.CharField(max_length=255)
    direction = models.ForeignKey(Direction, on_delete=models.CASCADE)
    date = models.DateTimeField(auto_now_add=True)
    active = models.BooleanField(default=True)

    def __str__(self):
        return self.number
